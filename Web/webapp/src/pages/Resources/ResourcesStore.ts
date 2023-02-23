import { makeAutoObservable, runInAction } from "mobx";
import { IResources } from "../../interfaces/resources";
import * as resourceApi from '../../api/modules/resources';

class ResourcesStore {
    resources: IResources[] = []
    currentPage = 1;
    totalPages = 0;
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
        runInAction(this.prefetchData)
    };

    async changePage(page: number) {
        this.currentPage = page;
        await this.prefetchData()
    };

    prefetchData = async () => {
        try {
            this.isLoading = true;
            const result = await resourceApi.getResourceByPage(this.currentPage);
            this.resources = result.data;
            this.totalPages = result.total_pages
        }
        catch (e) {
            if (e instanceof Error) {
                console.error(e.message);
            };
        };
        this.isLoading = false;
    };
};

export default ResourcesStore;