namespace UMeEngine
{
    public class Camera
    {
        private CameraType type;
        private (float width, float height) size;
        
        public CameraType Type => type;
        
        public Camera(CameraType type, float width, float height)
        {
            this.type = type;
            size = (width, height);
        }
    }
}