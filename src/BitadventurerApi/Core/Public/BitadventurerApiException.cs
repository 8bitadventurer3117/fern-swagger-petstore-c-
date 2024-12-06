using System;

#nullable enable

namespace BitadventurerApi;

/// <summary>
/// Base exception class for all exceptions thrown by the SDK.
/// </summary>
public class BitadventurerApiException(string message, Exception? innerException = null)
    : Exception(message, innerException) { }
