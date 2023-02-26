import { ILocation } from "./location";
import { IMeet } from "./meets";
import { IType } from "./type";

export interface IAnomaly {
    "id": number,
    "name": string,
    "type": IType,
    "locationPlace": ILocation,
    "meets": IMeet,
}