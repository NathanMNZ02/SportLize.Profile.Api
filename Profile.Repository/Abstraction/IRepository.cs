using System;
using SportLize.Profile.Api.Profile.Repository.Enumeration;
using SportLize.Profile.Api.Profile.Repository.Model;
using SportLize.Profile.Api.Profile.Shared.Dto;

namespace SportLize.Profile.Api.Profile.Repository.Abstraction
{
	public interface IRepository 
	{
        Task<int> SaveChanges(CancellationToken cancellationToken = default);

        #region INSERT
        Task<User> InsertUser(UserWriteDto userWriteDto, CancellationToken cancellationToken = default);
        Task<Post?> InsertPostForUser(int userId, PostWriteDto postWriteDto, CancellationToken cancellationToken = default);
        Task<User?> InsertFollowerToUser(int userId, int followerId, CancellationToken cancellationToken = default);
        Task<User?> InsertFollowingToUser(int userId, int followerId, CancellationToken cancellationToken = default);
        Task<Comment> InsertCommentForPost(int postId, CommentWriteDto commentWriteDto, CancellationToken cancellationToken = default);
        #endregion

        #region UPDATE
        Task<User> UpdateUser(UserReadDto userReadDto, CancellationToken cancellationToken = default);
        Task<Post> UpdatePost(PostReadDto postReadDto, CancellationToken cancellationToken = default);
        Task<Comment> UpdateComment(CommentReadDto commentReadDto, CancellationToken cancellationToken = default);
        #endregion

        #region GET
        Task<List<User>?> GetAllUser(CancellationToken cancellationToken = default);
        Task<List<User>?> GetAllFollowerOfUser(UserReadDto userReadDto, CancellationToken cancellationToken = default);
        Task<List<User>?> GetAllFollowingOfUser(UserReadDto userReadDto, CancellationToken cancellationToken = default);
        Task<List<Post>?> GetAllPostOfUser(int userId, CancellationToken cancellationToken = default);
        Task<List<Comment>?> GetAllCommentOfPost(int postId, CancellationToken cancellationToken = default);
        Task<User?> GetUser(int id, CancellationToken cancellationToken = default);
        Task<Post?> GetPost(int id, CancellationToken cancellationToken = default);
        Task<Comment?> GetComment(int id, CancellationToken cancellationToken = default);
        #endregion

        #region DELETE
        Task<User> DeleteUser(UserReadDto userReadDto, CancellationToken cancellationToken = default);
        Task<Post> DeletePost(PostReadDto postReadDto, CancellationToken cancellationToken = default);
        Task<Comment> DeleteComment(CommentReadDto commentReadDto, CancellationToken cancellationToken = default);
        #endregion

        #region TRANSACTIONALOUTBOX
        Task<IEnumerable<TransactionalOutbox>> GetAllTransactionalOutbox(CancellationToken cancellationToken = default);
        Task<TransactionalOutbox?> GetTransactionalOutboxByKey(long id, CancellationToken cancellationToken = default);
        Task DeleteTransactionalOutbox(long id, CancellationToken cancellationToken = default);
        Task InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox, CancellationToken cancellationToken = default); 
        #endregion
    }
}

