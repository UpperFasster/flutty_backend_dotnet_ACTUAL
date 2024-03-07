namespace fluttyBackend.Service.services.DeliverySystemService.Utils
{
    public enum DeliveryStatus
    {
        /// <summary>
        /// Order has been placed and is awaiting processing.
        /// </summary>
        SuccessfullyDelivered = 10,

        /// <summary>
        /// Order is currently being processed and prepared for shipment.
        /// </summary>
        Processing = 20,

        /// <summary>
        /// Order is cancelled by the client.
        /// </summary>
        Cancelled = 30,

        /// <summary>
        /// Order is pending to the client.
        /// </summary>
        Pending = 40,

        /// <summary>
        /// Order was rejected by admin or moderator.
        /// </summary>
        Denied = 50,

        /// <summary>
        /// Order was stolen or lost. The customer did not receive the goods.
        /// </summary>
        FailedDelivery = 0,

        /// <summary>
        /// The order is being held for some reason.
        /// </summary>
        OnHold = 70
    }
}