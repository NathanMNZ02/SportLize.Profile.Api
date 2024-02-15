using System;
using SportLize.Profile.Api.Profile.Repository.Enumeration;
using SportLize.Profile.Api.Profile.Repository.Model;
using SportLize.Profile.Api.Profile.Shared;
using SportLize.Profile.Api.Profile.Shared.Dto;

namespace SportLize.Profile.Api.Profile.Business.Abstraction
{
	public interface IBusiness
	{
        #region INSERT
        Task<UserReadDto> InsertUser(UserWriteDto userWriteDto, CancellationToken cancellationToken = default);
        Task<UserReadDto?> InserFollowerToUser(int userId, int followerId, CancellationToken cancellationToken = default);
        Task<UserReadDto?> InsertFollowingToUser(int userId, int followingId, CancellationToken cancellationToken = default);
        Task<PostReadDto> InsertPostForUser(int userId, PostWriteDto postWriteDto, CancellationToken cancellationToken = default);
        Task<CommentReadDto> InsertCommentForPost(int postId, CommentWriteDto commentWriteDto, CancellationToken cancellationToken = default);
        #endregion

        #region UPDATE
        Task<UserReadDto> UpdateUser(UserReadDto userReadDto, CancellationToken cancellationToken = default);
        Task<PostReadDto> UpdatePost(PostReadDto postReadDto, CancellationToken cancellationToken = default);
        Task<CommentReadDto> UpdateComment(CommentReadDto commentReadDto, CancellationToken cancellationToken = default);
        #endregion

        #region GET
        Task<List<UserReadDto>?> GetAllUser(CancellationToken cancellationToken = default);
        Task<List<UserReadDto>?> GetAllFollowerOfUser(int userId, CancellationToken cancellationToken = default);
        Task<List<UserReadDto>?> GetAllFollowingOfUser(int userId, CancellationToken cancellationToken = default);
        Task<List<PostReadDto>?> GetAllPostOfUser(int userId, CancellationToken cancellationToken = default);
        Task<List<CommentReadDto>?> GetAllCommentOfPost(int postId, CancellationToken cancellationToken = default);
        Task<UserReadDto?> GetUser(int id, CancellationToken cancellationToken = default);
        Task<PostReadDto?> GetPost(int id, CancellationToken cancellationToken = default);
        Task<CommentReadDto?> GetComment(int id, CancellationToken cancellationToken = default);
        #endregion

        #region DELETE
        Task<UserReadDto> DeleteUser(UserReadDto userReadDto, CancellationToken cancellationToken = default);
        Task<PostReadDto> DeletePost(PostReadDto postReadDto, CancellationToken cancellationToken = default);
        Task<CommentReadDto> DeleteComment(CommentReadDto commentReadDto, CancellationToken cancellationToken = default);
        #endregion
    }
}

