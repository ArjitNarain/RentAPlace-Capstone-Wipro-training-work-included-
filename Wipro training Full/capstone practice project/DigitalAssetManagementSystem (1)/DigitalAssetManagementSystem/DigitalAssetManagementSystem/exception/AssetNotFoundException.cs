namespace DigitalAssetManagementSystem.exception
{
    public class AssetNotFoundException : Exception
    {
        public AssetNotFoundException(string message) : base(message) { }
    }
}