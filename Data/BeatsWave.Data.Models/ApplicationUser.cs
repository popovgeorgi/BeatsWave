﻿// ReSharper disable VirtualMemberCallInConstructor
namespace BeatsWave.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BeatsWave.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Beat> Beats { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Follower> Followers { get; set; }

        public virtual ICollection<Follower> Following { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

        public virtual ICollection<CloudinaryImage> Images { get; set; }

        public virtual ICollection<CloudinaryBeat> CloudinaryBeats { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
