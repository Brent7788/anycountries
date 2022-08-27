import type Pagination from "../models/Pagination";

export default class ComponentHelper {

    private static readonly MAX_RESULT = 5;

    public static async copyFirstFive(allResult: Promise<[]>): Promise<any[]> {
        const values = await allResult;
        const firstFive = [];
        for (let i = 0; i < this.MAX_RESULT; i++) {
            firstFive[i] = values[i];
        }
        return  new Promise<any[]>((resolve, reject) => resolve([...firstFive]));
    }

    public static async nextResult(pagination: Pagination): Promise<Pagination> {
        pagination.start += this.MAX_RESULT;
        return this.pagination(pagination);
    }

    public static async previseResult(pagination: Pagination): Promise<Pagination> {
        pagination.start -= this.MAX_RESULT;
        return this.pagination(pagination);
    }

    private static async pagination(pagination: Pagination): Promise<Pagination> {
        const values = await pagination.allResult;

        const firstFive = [];

        for (let i = pagination.start; i < (pagination.start + this.MAX_RESULT); i++) {
            firstFive[i - pagination.start] = values[i];
        }

        pagination.result = new Promise<any[]>((resolve, reject) => resolve([...firstFive]));

        return pagination;
    }
}
