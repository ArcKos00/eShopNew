import { makeAutoObservable, runInAction } from "mobx";
import * as catalogbff from "../../api/modules/catalogApi"
import { IAnomaly } from "../../interfaces/anomaly";
import { IMeet } from "../../interfaces/meets";
import { IType } from "../../interfaces/type"

class Filter {
    Types: IType[] = []
    Meets: IMeet[] = []
    Anomalies: IAnomaly[] = []

    filter: Record<string, number> = {}

    constructor() {
        makeAutoObservable(this);
        this.getAnomalies();
        this.getMeets();
        this.getTypes();
    }

    getTypes = async () => {
        this.Types = await catalogbff.getTypes()
    }

    getMeets = async () => {
        this.Meets = await catalogbff.getMeets();
    }

    getAnomalies = async () => {
        this.Anomalies = await catalogbff.getAnomalies();
    }

    setType = async (typeId: number) => {
        this.filter.Type = typeId;
    }

    setMeet = async (meetId: number) => {
        this.filter.Meets = meetId;
    }

    setAnomaly = async (anomalyId: number) => {
        this.filter.Anomaly = anomalyId;
    }

    clearFilter = async () => {
        this.filter = {}
    }
}

export default Filter;