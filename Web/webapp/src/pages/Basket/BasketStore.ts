import { makeAutoObservable, runInAction } from "mobx";
import * as basketApi from "../../api/modules/basketApi";
import { IItem } from "../../interfaces/Item";

class BasketStore {
    basket: IItem[] = [];
    totalCost = 0;
    isLoading = false;
    userId = '';

    constructor() {
        makeAutoObservable(this);
        runInAction(this.prefetchData);
    }
    private check = (userId: string | undefined) => {
        if (userId) {
            this.userId = userId;
        }
        else {
            this.userId = "Sidorovich"
        }
    }

    prefetchData = async () => {
        try {
            this.isLoading = true;
            this.check(this.userId);
            const result = await basketApi.getBasket(this.userId);
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
        this.check(userId)
        await basketApi.add(this.userId, id, name, cost);
    }

    async remove(userId: string, id: number) {
        this.check(userId);
        await basketApi.remove(this.userId, id);
        this.prefetchData();
    }

    async makeAnOrder(userId: string) {
        this.check(userId);
        await basketApi.makeAnOrder(this.userId);
        this.basket = [];
        this.totalCost = 0;
    }

    async get(userId: string) {
        this.check(userId);
        const result = await basketApi.getBasket(this.userId);
        this.totalCost = result.totalCost;
        this.basket = result.basketList;
    }

    async clear(userId: string) {
        this.check(userId);
        await basketApi.clear(this.userId);
        this.prefetchData()
    }
}

export default BasketStore;