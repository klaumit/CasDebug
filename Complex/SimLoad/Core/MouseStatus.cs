namespace SimLoad.Core
{
    public record MouseStatus(
        uint A1,
        int A2,
        int A3,
        int R
    );

    public record RamArea(
        uint A1,
        uint A2,
        byte R
    );
}