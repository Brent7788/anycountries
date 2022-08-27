export default class Pagination {
    public allResult: Promise<[]>;
    public result: Promise<any[]>;
    public start: number;

    constructor(start: number, allResult: Promise<[]>) {
        this.start = start;
        this.allResult = allResult;
    }
}
