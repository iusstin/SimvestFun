namespace SimvestFun.ApplicationCore.Models
{
    public class UserPagingModel
    {
        public List<UserModel> Users { get; set; }
        public int TotalPages { get; set; }
        public int TotalSize { get; set; }
    }
}
