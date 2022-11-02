public class ViewModel
{
    public bool Displayed { get; set; } = false;

    public string Log { get; set; }

    public void Shown()
    {
        Log += "shown\r\n";
    }

    public void Hidden()
    {
        Log += "hidden\r\n";
    }
}