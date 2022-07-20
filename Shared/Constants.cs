public static class Constants
{
    public static string? AWS_ACCESS_KEY_ID = "";

    public static string? AWS_SECRET_ACCESS_KEY = "";

    static Constants()
    {
        if (string.IsNullOrWhiteSpace(AWS_ACCESS_KEY_ID))
        {
            AWS_ACCESS_KEY_ID = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
        }

        if (string.IsNullOrWhiteSpace(AWS_SECRET_ACCESS_KEY))
        {
            AWS_SECRET_ACCESS_KEY = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
        }

        if (string.IsNullOrWhiteSpace(AWS_ACCESS_KEY_ID) || string.IsNullOrWhiteSpace(AWS_SECRET_ACCESS_KEY))
        {
            throw new Exception("AWS_ACCESS_KEY_ID and AWS_SECRET_ACCESS_KEY are not found or invalid");
        }
    }
}