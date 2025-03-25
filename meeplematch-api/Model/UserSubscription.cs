using System;
using System.Collections.Generic;

namespace meeplematch_api.Model;

public partial class UserSubscription
{
    public int IdUserSubscription { get; set; }

    public int UserId { get; set; }

    public int SubscriptionPlanId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
