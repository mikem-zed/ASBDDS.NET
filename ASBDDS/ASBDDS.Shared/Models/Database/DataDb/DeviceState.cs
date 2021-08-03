namespace ASBDDS.Shared.Models.Database.DataDb
{
    /// <summary>
    /// 1 - Creating state
    /// 2 - Provisioning state
    /// 3 - Device is powered on
    /// 4 - Device is powered off
    /// 5 - Erase device storage
    /// </summary>
    public enum DeviceState
    {
        /// <summary>
        /// Creating state
        /// </summary>
        CREATING,
        /// <summary>
        /// Provisioning state
        /// </summary>
        PROVISIONING,
        /// <summary>
        /// Device is powered on
        /// </summary>
        POWERON,
        /// <summary>
        /// Device is powered off
        /// </summary>
        POWEROFF,
        /// <summary>
        /// Erase device storage
        /// </summary>
        ERASING,
    }
}
