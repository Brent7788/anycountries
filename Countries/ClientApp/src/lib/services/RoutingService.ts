import Condition from "../tools/Condition";

export default class RoutingService {

    public static previsePage: string;
    private _fullPath: string;
    public paramObject: any;

    constructor(fullPath: string = "") {
        this._fullPath = (fullPath === "") ? window.location.hash : fullPath;
        this.paramObject = RoutingService.createParamObject(this.query());
    }

    private static createParamObject(query: string): any {
        let paramObject: any = {};

        if (query) {
            const queryChunks = query.split("&");

            let previousParamName = "";

            for (let queryChunk of queryChunks) {
                let values = queryChunk.split("=");

                if (values.length > 2) {
                    const newValues = [values[0]];
                    let combine = "";
                    values.forEach((value, index) => {
                        if (index > 0) {
                            combine = combine + value;
                        }
                    });
                    newValues.push(combine);
                    values = newValues;
                }

                if (values.length === 2) {
                    previousParamName = values[0];
                    paramObject[previousParamName] = values[1];
                } else if (values.length === 1) {
                    paramObject[previousParamName] = `${paramObject[previousParamName]}&${values[0]}`;
                }
            }

        }

        return paramObject;
    }

    public refersFullPath(): void {
        this._fullPath = window.location.hash;
    }

    public get fullPath(): string {
        return this._fullPath;
    }

    public set fullPath(value: string) {
        this._fullPath = value;
    }

    private fullPathSplit(): string[] {
        return this._fullPath.split("?");
    }

    public lastPath() {
        let d = this.path().split("/");
        return d[d.length - 1];
    }

    public path(): string {
        return this.fullPathSplit()[0];
    }

    public query(): string {
        return this.fullPathSplit()[1];
    }

    public static gotoPreviousPage(): void {
        if (Condition.isStringEmpty(this.previsePage))
            return;

        this.goto(this.previsePage.replace("#", ""))
    }

    public static goto(path: string): void {
        window.location.hash = `#${path}`;
    }
}
