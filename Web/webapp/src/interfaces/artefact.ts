import { IAnomaly } from "./anomaly";
import { ICharacteristic } from "./characteristic";
import { IMeet } from "./meets";
import { IType } from "./type";

export interface IArtefact {
    'id': number,
    'name': string,
    'cost': number,
    'nature': string,
    'imageUrl': string,
    'values': ICharacteristic,
    'anomaly': IAnomaly
    'type': IType,
    'meets': IMeet,
};