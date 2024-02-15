using System;
using System.Collections;
using AutoMapper;
using SportLize.Profile.Api.Profile.Business.Abstraction;
using SportLize.Profile.Api.Profile.Business.Factory;
using SportLize.Profile.Api.Profile.Repository.Abstraction;
using SportLize.Profile.Api.Profile.Repository.Model;
using SportLize.Profile.Api.Profile.Shared.Dto;

namespace SportLize.Profile.Api.Profile.Business
{
    public class Business : IBusiness
    {
        private IMapper _mapper;
        private readonly IRepository _repository;

        public Business(IRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        #region INSERT
        public async Task<UserReadDto> InsertUser(UserWriteDto userWriteDto, CancellationToken cancellationToken = default)
        {
            var result = _mapper.Map<UserReadDto>(await _repository.InsertUser(userWriteDto, cancellationToken));
            await _repository.SaveChanges(cancellationToken);

            await _repository.InsertTransactionalOutbox(TransactionalOutboxFactory.CreateInsert(result), cancellationToken);
            await _repository.SaveChanges(cancellationToken);
            return result;
        }

        public async Task<UserReadDto?> InserFollowerToUser(int userId, int followerId, CancellationToken cancellationToken = default)
        {
            var user = await _repository.InsertFollowerToUser(userId, followerId, cancellationToken);
            await _repository.SaveChanges(cancellationToken);

            await _repository.InsertFollowingToUser(followerId, userId, cancellationToken);
            await _repository.SaveChanges(cancellationToken);
            return _mapper.Map<UserReadDto?>(user);
        }

        public async Task<UserReadDto?> InsertFollowingToUser(int userId, int followingId, CancellationToken cancellationToken = default)
        {
            var user = await _repository.InsertFollowingToUser(userId, followingId, cancellationToken);
            await _repository.SaveChanges(cancellationToken);

            await _repository.InsertFollowerToUser(followingId, userId, cancellationToken);
            await _repository.SaveChanges(cancellationToken);
            return _mapper.Map<UserReadDto?>(user);
        }

        public async Task<PostReadDto> InsertPostForUser(int userId, PostWriteDto postWriteDto, CancellationToken cancellationToken = default)
        {
            var result = _mapper.Map<PostReadDto>(await _repository.InsertPostForUser(userId, postWriteDto, cancellationToken));
            await _repository.SaveChanges(cancellationToken);
            return result;
        }

        public async Task<CommentReadDto> InsertCommentForPost(int postId, CommentWriteDto commentWriteDto, CancellationToken cancellationToken = default)
        {
            var result = _mapper.Map<CommentReadDto>(await _repository.InsertCommentForPost(postId, commentWriteDto, cancellationToken));
            await _repository.SaveChanges(cancellationToken);
            return result;
        }
        #endregion

        #region UPDATE
        public async Task<UserReadDto> UpdateUser(UserReadDto userReadDto, CancellationToken cancellationToken = default)
        {
            var result = _mapper.Map<UserReadDto>(await _repository.UpdateUser(userReadDto, cancellationToken));
            await _repository.SaveChanges(cancellationToken);

            await _repository.InsertTransactionalOutbox(TransactionalOutboxFactory.CreateUpdate(result), cancellationToken);
            await _repository.SaveChanges(cancellationToken);
            return result;
        }

        public async Task<PostReadDto> UpdatePost(PostReadDto postReadDto, CancellationToken cancellationToken = default)
        {
            var result = _mapper.Map<PostReadDto>(await _repository.UpdatePost(postReadDto, cancellationToken));
            await _repository.SaveChanges(cancellationToken);
            return result;
        }

        public async Task<CommentReadDto> UpdateComment(CommentReadDto commentReadDto, CancellationToken cancellationToken = default)
        {
            var result = _mapper.Map<CommentReadDto>(await _repository.UpdateComment(commentReadDto, cancellationToken));
            await _repository.SaveChanges(cancellationToken);
            return result;
        }
        #endregion

        #region GET
        public async Task<List<UserReadDto>?> GetAllUser(CancellationToken cancellationToken = default)
        {

            var users = await _repository.GetAllUser(cancellationToken);
            if (users is null)
                return null;

            var usersDto = new List<UserReadDto>();
            Parallel.ForEach(users, user =>
            {
                usersDto.Add(_mapper.Map<UserReadDto>(user));
            });
            return usersDto;
        }

        public async Task<List<UserReadDto>?> GetAllFollowerOfUser(int userId, CancellationToken cancellationToken = default)
        {
            var userDto = _mapper.Map<UserReadDto?>(await _repository.GetUser(userId, cancellationToken));
            if(userDto is null)
                throw new Exception($"User with id:{userId} not exist");

            return _mapper.Map<List<UserReadDto>?>(await _repository.GetAllFollowerOfUser(userDto));
        }

        public async Task<List<UserReadDto>?> GetAllFollowingOfUser(int userId, CancellationToken cancellationToken = default)
        {
            var userDto = _mapper.Map<UserReadDto?>(await _repository.GetUser(userId, cancellationToken));
            if (userDto is null)
                throw new Exception($"User with id:{userId} not exist");

            return _mapper.Map<List<UserReadDto>?>(await _repository.GetAllFollowingOfUser(userDto));
        }

        public async Task<List<PostReadDto>?> GetAllPostOfUser(int userId, CancellationToken cancellationToken = default)
        {
            var posts = await _repository.GetAllPostOfUser(userId, cancellationToken);
            if (posts is null)
                return null;

            var postsDto = new List<PostReadDto>();
            Parallel.ForEach(posts, post =>
            {
                postsDto.Add(_mapper.Map<PostReadDto>(post));
            });
            return postsDto;
        }

        public async Task<List<CommentReadDto>?> GetAllCommentOfPost(int postId, CancellationToken cancellationToken = default)
        {
            var comments = await _repository.GetAllCommentOfPost(postId, cancellationToken);
            if (comments is null)
                return null;

            var commentsDto = new List<CommentReadDto>();
            Parallel.ForEach(comments, comment =>
            {
                commentsDto.Add(_mapper.Map<CommentReadDto>(comment));
            });
            return commentsDto;
        }

        public async Task<UserReadDto?> GetUser(int id, CancellationToken cancellationToken = default) =>
            _mapper.Map<UserReadDto>(await _repository.GetUser(id, cancellationToken));

        public async Task<PostReadDto?> GetPost(int id, CancellationToken cancellationToken = default) =>
            _mapper.Map<PostReadDto>(await _repository.GetPost(id, cancellationToken));
        public async Task<CommentReadDto?> GetComment(int id, CancellationToken cancellationToken = default) =>
            _mapper.Map<CommentReadDto>(await _repository.GetComment(id, cancellationToken));
        #endregion

        #region DELETE
        public async Task<UserReadDto> DeleteUser(UserReadDto userReadDto, CancellationToken cancellationToken = default)
        {
            var result = _mapper.Map<UserReadDto>(await _repository.DeleteUser(userReadDto, cancellationToken));
            await _repository.SaveChanges(cancellationToken);

            await _repository.InsertTransactionalOutbox(TransactionalOutboxFactory.CreateDelete(result), cancellationToken);
            await _repository.SaveChanges(cancellationToken);
            return result;
        }

        public async Task<PostReadDto> DeletePost(PostReadDto postReadDto, CancellationToken cancellationToken = default)
        {
            var result = _mapper.Map<PostReadDto>(await _repository.DeletePost(postReadDto, cancellationToken));
            await _repository.SaveChanges(cancellationToken);
            return result;
        }

        public async Task<CommentReadDto> DeleteComment(CommentReadDto commentReadDto, CancellationToken cancellationToken = default)
        {
            var result = _mapper.Map<CommentReadDto>(await _repository.DeleteComment(commentReadDto, cancellationToken));
            await _repository.SaveChanges(cancellationToken);
            return result;
        }
        #endregion

    }
}

