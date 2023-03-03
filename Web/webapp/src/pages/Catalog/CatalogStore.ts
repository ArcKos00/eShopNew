import { makeAutoObservable, runInAction } from "mobx";
import { getItems } from "../../api/modules/catalogApi";
import { IArtefact } from "../../interfaces/artefact";

class ArtefactStore {
    artefacts: IArtefact[] = [];
    currentPage = 1;
    totalPages = 0;
    pageSize = 8;
    filter: {
        "Meets": number | null,
        "Anomaly": number | null;
        "Type": number | null;
    }
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
        this.filter = null!;
        runInAction(this.prefetchData)
    };

    async changePage(page: number) {
        this.currentPage = page;
        await this.prefetchData();
    };

    async reset() {
        this.currentPage = 1;
        this.filter = null!;
        await this.prefetchData();
    }

    async back() {
        await this.prefetchData();
    }

    async makeFilter(filter: {
        "Meets": number | null,
        "Anomaly": number | null;
        "Type": number | null;
    }) {
        this.filter = filter;
        await this.prefetchData();
    }

    prefetchData = async () => {
        try {
            this.isLoading = true;
            const pageIndex = this.currentPage - 1;
            const result = await getItems(pageIndex, this.pageSize, this.filter);
            this.artefacts = result.data;
            this.totalPages = Math.ceil(result.count / this.pageSize);
        }
        catch (e) {
            if (e instanceof Error) {
                console.error(e.message);
            }
        }
        this.isLoading = false;
    }
}

export default ArtefactStore;