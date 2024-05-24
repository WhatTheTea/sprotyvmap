namespace WhatTheTea.SprotyvMap.Data.Primitives;

public record District(int Id, string Name, IEnumerable<EquipmentCentre> EquipmentCentres);