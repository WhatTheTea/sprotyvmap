namespace WhatTheTea.SprotyvMap.Shared.Primitives;

public record District(int Id, string Name, IEnumerable<EquipmentCentre> EquipmentCentres);