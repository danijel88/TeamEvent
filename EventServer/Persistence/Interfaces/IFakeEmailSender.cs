﻿namespace EventServer.Persistence.Interfaces;

public interface IFakeEmailSender
{
    Task SendEmailAsync(string[] to, string from, string subject, string body);
}