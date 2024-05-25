using System.Text.Json.Serialization;

namespace WhatTheTea.Visicom.Geocoder.Data;

public record GeoCentroid(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("coordinates")] IReadOnlyList<double> Coordinates
);