import { makeAutoObservable, runInAction } from "mobx";
import * as basketApi from "../../api/modules/basketApi";
import { IOrderItem } from "../../interfaces/item";
import { AppStoreContext } from "../../App";
import { useContext } from "react";

class BasketStore {
    basket: IOrderItem[] = [];
    user = useContext(AppStoreContext).authStore.user;
    totalCost = 0;
    isLoading = false;
    defaultUser = "Sidorovich"

    constructor() {
        makeAutoObservable(this);
        runInAction(this.prefetchData)
    }

    prefetchData = async () => {
        try {
            this.isLoading = true;
            var userId: string;
            if (!!this.user) {
                userId = this.user?.profile.sub;
            }
            else {
                userId = this.defaultUser;
            }
            const result = await basketApi.getBasket(userId);
            this.totalCost = result.totalCost;
            this.basket = result.basketList;
        }
        catch (e) {
            if (e instanceof Error) {
                console.error(e.message);
            }
        }
        this.isLoading = false;
    }

    async add(userId: string, id: number, name: string, cost: number) {
        await basketApi.add(userId, id, name, cost);
        await this.prefetchData();
    }

    async remove(userId: string, id: number) {
        await basketApi.remove(userId, id);
        await this.prefetchData();
    }

    async makeAnOrder(userId: string) {
        await basketApi.makeAnOrder(userId);
        await this.prefetchData();
    }
}

export default BasketStore;