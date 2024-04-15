namespace SimvestFun.ApplicationCore.Entities
{
    public class UserAction
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string ActionType { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Description { get; set; }
        public ApplicationUser User { get; set; }
    }
}
