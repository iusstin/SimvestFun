namespace SimvestFun.ApplicationCore.Models
{
    public class UserActionModel
    {
        public string ApplicationUserId { get; set; }
        public string ActionType { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Description { get; set; }
        public UserModel User { get; set; }

    }
}
