using SportLize.Profile.Api.Profile.Repository.Abstraction;
using SportLize.Profile.Api.Profile.Repository.Model;
using SportLize.Profile.Api.Profile.Shared.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace SportLize.Profile.Api.Profile.Repository
{
    public class Repository : IRepository
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ProfileDbContext _profileDbContext;

        public Repository(ProfileDbContext profileDbContext, IMapper mapper, ILogger<Repository> logger)
        {
            _mapper = mapper;
            _profileDbContext = profileDbContext;
            _logger = logger;
            _profileDbContext.Database.Migrate();

            _profileDbContext.SavedChanges += _profileDbContext_SavedChanges;
            _profileDbContext.SaveChangesFailed += _profileDbContext_SaveChangesFailed;
        }

        private void _profileDbContext_SavedChanges(object? sender, SavedChangesEventArgs e)
        {
            _logger.LogInformation($"Saved of {e.EntitiesSavedCount} entity successfully");
        }

        private void _profileDbContext_SaveChangesFailed(object? sender, SaveChangesFailedEventArgs e)
        {
            _logger.LogError(e.Exception.Message);
            _logger.LogError(e.Exception.StackTrace);
        }

        public async Task<int> SaveChanges(CancellationToken cancellationToken = default) =>
            await _profileDbContext.SaveChangesAsync(cancellationToken);

        #region INSERT
        public async Task<User> InsertUser(UserWriteDto userWriteDto, CancellationToken cancellationToken = default)
        {
            var result = await _profileDbContext.User.AddAsync(_mapper.Map<User>(userWriteDto), cancellationToken); 
            return result.Entity;
        }

        public async Task<User?> InsertFollowerToUser(int userId, int followerId, CancellationToken cancellationToken = default)
        {
            var user = await GetUser(userId, cancellationToken);
            var follower = await GetUser(followerId);

            if (user is not null)
                if(follower is not null)
                if (user.Followers.FirstOrDefault(x => x.Id == followerId) is null)
                    user.Followers.Add(follower);

            return user;
        }

        public async Task<User?> InsertFollowingToUser(int userId, int followingId, CancellationToken cancellationToken = default)
        {
            var user = await GetUser(userId, cancellationToken);
            var following = await GetUser(followingId);

            if (user is not null)
                if (following is not null)
                    if (user.Followers.FirstOrDefault(x => x.Id == followingId) is null)
                        user.Followers.Add(following);

            return user;
        }

        public async Task<Post> InsertPostForUser(int userId, PostWriteDto postWriteDto, CancellationToken cancellationToken = default)
        {
            var post = _mapper.Map<Post>(postWriteDto);

            var user = await GetUser(userId);
            if (user is not null)
            {
                post.UserId = user.Id;
                post.User = user;
                user.Posts.Add(post);
            }

            return post;
        }

        public async Task<Comment> InsertCommentForPost(int postId, CommentWriteDto commentWriteDto, CancellationToken cancellationToken = default)
        {           
            var comment = _mapper.Map<Comment>(commentWriteDto);

            var post = await GetPost(postId, cancellationToken);
            if (post is not null)
            {
                comment.PostId = postId;
                comment.Post = post;
                post.Comments.Add(comment);
            }

            return comment;
        }
        #endregion

        #region UPDATE
        public async Task<User> UpdateUser(UserReadDto userReadDto, CancellationToken cancellationToken = default)
        {
            var newUser = _mapper.Map<User>(userReadDto);
            var oldUser = await _profileDbContext.User.Include(s => s.Followers).Include(s => s.Posts).FirstOrDefaultAsync(s => s.Id == newUser.Id);
            if(oldUser is not null)
            {
                oldUser.Actor = newUser.Actor;
                oldUser.Nickname = newUser.Nickname;
                oldUser.Name = newUser.Name;
                oldUser.Surname = newUser.Surname;
                oldUser.Password = newUser.Password;
                oldUser.Description = newUser.Description;
                oldUser.DateOfBorn = newUser.DateOfBorn;
            }
            return newUser;
        }

        public async Task<Post> UpdatePost(PostReadDto postReadDto, CancellationToken cancellationToken = default)
        {
            var newPost = _mapper.Map<Post>(postReadDto);
            var oldPost = await _profileDbContext.Post.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == newPost.Id, cancellationToken);
            if (oldPost is not null)
            {
                oldPost.Media = newPost.Media;
                oldPost.Like = newPost.Like;
                oldPost.PubblicationDate = newPost.PubblicationDate;
                oldPost.Description = newPost.Description;
            }
            return newPost;
        }

        public async Task<Comment> UpdateComment(CommentReadDto commentReadDto, CancellationToken cancellationToken = default)
        {
            var newComment = _mapper.Map<Comment>(commentReadDto);
            var oldComment = await _profileDbContext.Comment.FirstOrDefaultAsync(c => c.Id == newComment.Id, cancellationToken);
            if (oldComment is not null)
            {
                oldComment.Text = newComment.Text;
                oldComment.Like = newComment.Like;
                oldComment.PubblicationDate = newComment.PubblicationDate;
            }
            return newComment;
        }
        #endregion

        #region GET
        public async Task<List<User>?> GetAllUser(CancellationToken cancellationToken = default) =>
            await _profileDbContext.User.ToListAsync(cancellationToken);

        public async Task<List<User>?> GetAllFollowerOfUser(UserReadDto userReadDto, CancellationToken cancellationToken = default)
        {
            var user = await _profileDbContext.User.Include(x=> x.Followers).FirstOrDefaultAsync(x=> x.Id == userReadDto.Id);
            if (user is not null)
                return user.Followers;
            return null;
        }

        public async Task<List<User>?> GetAllFollowingOfUser(UserReadDto userReadDto, CancellationToken cancellationToken = default)
        {
            var user = await GetUser(userReadDto.Id, cancellationToken);
            if (user is not null)
                return _profileDbContext.User.Where(x=> x.Followers.Contains(user)).ToList();
            return null;
        }

        public async Task<List<Post>?> GetAllPostOfUser(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _profileDbContext.User.Include(u => u.Posts).FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            return user?.Posts;
        }

        public async Task<List<Comment>?> GetAllCommentOfPost(int postId, CancellationToken cancellationToken = default)
        {
            var post = await _profileDbContext.Post.Include(u => u.Comments).FirstOrDefaultAsync(u => u.Id == postId, cancellationToken);
            return post?.Comments;
        }

        public async Task<User?> GetUser(int id, CancellationToken cancellationToken = default) =>
            await _profileDbContext.User.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        public async Task<Post?> GetPost(int id, CancellationToken cancellationToken = default) =>
            await _profileDbContext.Post.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        public async Task<Comment?> GetComment(int id, CancellationToken cancellationToken = default) =>
            await _profileDbContext.Comment.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        #endregion

        #region DELETE
        public async Task<User> DeleteUser(UserReadDto userReadDto, CancellationToken cancellationToken = default)
        {
            var user = _mapper.Map<User>(userReadDto);
            _profileDbContext.User.Remove(user);
            return user;
        }

        public async Task<Post> DeletePost(PostReadDto postReadDto, CancellationToken cancellationToken = default)
        {
            var post = _mapper.Map<Post>(postReadDto);
            _profileDbContext.Post.Remove(post);
            return post;
        }

        public async Task<Comment> DeleteComment(CommentReadDto commentReadDto, CancellationToken cancellationToken = default)
        {
            var comment = _mapper.Map<Comment>(commentReadDto);
            _profileDbContext.Comment.Remove(comment);
            return comment;
        }
        #endregion

        #region TRANSACTIONAL OUTBOX
        public async Task<IEnumerable<TransactionalOutbox>> GetAllTransactionalOutbox(CancellationToken cancellationToken = default) => 
            await _profileDbContext.TransactionalOutboxes.ToListAsync(cancellationToken);

        public async Task<TransactionalOutbox?> GetTransactionalOutboxByKey(long id, CancellationToken cancellationToken = default) =>
            await _profileDbContext.TransactionalOutboxes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task DeleteTransactionalOutbox(long id, CancellationToken cancellationToken = default) =>
            _profileDbContext.TransactionalOutboxes.Remove((await GetTransactionalOutboxByKey(id, cancellationToken)) ?? throw new ArgumentException($"TransactionalOutbox con id {id} non trovato", nameof(id)));

        public async Task InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox, CancellationToken cancellationToken = default) =>
            await _profileDbContext.TransactionalOutboxes.AddAsync(transactionalOutbox);
        #endregion
    }
}