import { makeAutoObservable, runInAction } from "mobx";
import { IOrder } from "../../interfaces/order";
import { getItems } from '../../api/modules/orderApi'

class OrdersStore {
    orders: IOrder[] = []
    countPage = 0;
    currentPage = 1;
    pageSize = 50;
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
        runInAction(this.prefetchData);
    }

    prefetchData = async () => {
        try {
            this.isLoading = true;
            const result = await getItems((this.currentPage - 1), this.pageSize)
            this.orders = result.orders;
            this.countPage = Math.ceil(result.ordersCount / this.pageSize);
        }
        catch (e) {
            if (e instanceof Error) {
                console.error(e.message);
            }
        }
        this.isLoading = false;
    }

    async changePage(page: number) {
        this.currentPage = page;
        await this.prefetchData();
    }

    async getOrders() {
        await this.prefetchData();
        console.log(this.orders)
    }
}

export default OrdersStore;