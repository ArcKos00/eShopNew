import { makeAutoObservable, runInAction } from "mobx";
import { IArtefact } from "../../interfaces/artefacts";
import * as artefactApi from '../../api/modules/artefacts'

class UserStore {
    users: IArtefact[] = [];
    pageSize = 6;
    currentPage = 1;
    totalPages = 0;
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
        runInAction(this.prefetchData);
    };

    async changePage(page: number) {
        this.currentPage = page;
        await this.prefetchData();
    }

    prefetchData = async () => {
        try {
            var pagIndex = this.currentPage
            var pagSize = this.pageSize
            this.isLoading = true;
            const result = await artefactApi.getArtefactsByPage({ PageIndex: this.currentPage, PageSize: this.pageSize, Filters: null });
            this.users = result.data;
            this.totalPages = result.total_pages;
        }
        catch (e) {
            if (e instanceof Error) {
                console.error(e.message);
            }
        };
        this.isLoading = false;
    };
};

export default UserStore;