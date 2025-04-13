using System;
using System.Collections.Generic;

namespace meeplematch_api.Model;

public partial class SubscriptionPlan
{
    public int IdSubscriptionPlan { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Duration { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
}
