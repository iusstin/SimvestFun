using System.ComponentModel.DataAnnotations.Schema;

namespace SimvestFun.ApplicationCore.Entities
{
    public class Follow
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public string FollowedUserId { get; set; }
        [ForeignKey("FollowedUserId")]
        public virtual ApplicationUser FollowedUser{ get; set; }
    }
}
