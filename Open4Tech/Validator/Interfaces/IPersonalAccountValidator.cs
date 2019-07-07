namespace Open4Tech.Validator.Interfaces
{
    interface IPersonalAccountValidator <T>
    {
        T ValidatePassword(string password);

        T ValidateEmail(string emailstring, string emailSubject, string emailContent);
    }
}
