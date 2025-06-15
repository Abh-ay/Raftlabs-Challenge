public class PagerResponse<T>
{

    public int Page { get; set; }
    public int Per_Page { get; set; }
    public int Total { get; set; }
    public int Total_Pages { get; set; }
    public List<T> Data { get; set; }
    public SupportInfo Support { get; set; }
}

public class SupportInfo
{
    public string Url { get; set; }
    public string Text { get; set; }
}

public class SingleUserResponse<T>
{
    public T Data { get; set; }
    public SupportInfo Support { get; set; }
}