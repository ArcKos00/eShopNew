import { makeAutoObservable, runInAction } from "mobx";
import * as basketApi from "../../api/modules/basketApi";
import { IItem } from "../../interfaces/Item";

class BasketStore {
    basket: IItem[] = [];
    totalCost = 0;
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
        runInAction(this.prefetchData);
    }

    prefetchData = async () => {
        try {
            this.isLoading = true;
            const result = await basketApi.getBasket();
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

    async add(id: number, name: string, cost: number) {
        await basketApi.add(id, name, cost);
    }

    async remove(id: number) {
        await basketApi.remove(id);
        this.prefetchData();
    }

    async makeAnOrder() {
        await basketApi.makeAnOrder();
        this.basket = [];
        this.totalCost = 0;
    }

    async get() {
        const result = await basketApi.getBasket();
        this.totalCost = result.totalCost;
        this.basket = result.basketList;
    }

    async clear() {
        await basketApi.clear();
        this.prefetchData()
    }
}

export default BasketStore;