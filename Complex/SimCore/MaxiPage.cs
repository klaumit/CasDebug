namespace SimCore
{
    public record MaxiPage(
        string Addr,
        string Size,
        string Attr,
        string Hex = null,
        string Err = null
    );
}