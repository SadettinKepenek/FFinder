﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FFinder.Core.DataTransferObjects.User
{
    public class UserDetailDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string? AboutMe { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? ViberUrl { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public string School { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Entity.Concrete.Follower> Followers { get; set; }
        public virtual ICollection<Entity.Concrete.Comment> Comments { get; set; }
        public virtual ICollection<Entity.Concrete.Post> Posts { get; set; }
        public virtual ICollection<Entity.Concrete.PostRate> PostRates { get; set; }
        public virtual ICollection<Entity.Concrete.CommentRate> CommentRates { get; set; }
    }
}
