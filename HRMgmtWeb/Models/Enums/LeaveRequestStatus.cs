namespace HRMgmtWeb.Models.Enums
{
    public enum LeaveRequestStatus
    {
        Pending,    // Request submitted but not reviewed
        Approved,   // Manager approved the leave
        Rejected,   // Manager rejected the leave
        Cancelled   // Employee cancelled the request
    }
}
