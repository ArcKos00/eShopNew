import { makeAutoObservable, runInAction } from "mobx";
import { IUser } from "../../interfaces/users";
import * as userApi from '../../api/modules/users'

class UserStore {
    users: IUser[] = [];
    currentPage = 1;
    totalPages = 0;
    isLoading = false;
    openForm = false;

    constructor() {
        makeAutoObservable(this);
        runInAction(this.prefetchData);
    };

    handleOpenForm = () => {
        this.openForm = !this.openForm;
    };

    async changePage(page: number) {
        this.currentPage = page;
        await this.prefetchData();
    }

    prefetchData = async () => {
        try {
            this.isLoading = true;
            const result = await userApi.getUserByPage(this.currentPage);
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