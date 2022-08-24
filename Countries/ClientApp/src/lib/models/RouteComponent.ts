import Condition from "../tools/Condition";

export default class RouteComponent {

    private _route: string;
    private _params: string[] | undefined;
    private _component: any;
    private _componentParam: any | undefined;

    constructor(component: any, route: string, params: string[] | undefined = undefined, componentParam: any | undefined = undefined) {
        this._route = `#/${route}`;
        this._params = params;
        this._component = component;
        this._componentParam = componentParam;
    }

    public isParameterInFullPath(fullPath: string): boolean {

        if (Condition.isNothing(this.params))
            return false;

        for (const param of this.params) {
            if (Condition.stringContain(fullPath, `${param}=`)) {
                return true;
            }
        }

        return false;
    }

    public get route(): string {
        return this._route;
    }

    public set route(value: string) {
        this._route = value;
    }

    public get params(): string[] | undefined {
        return this._params;
    }

    public set params(value: string[] | undefined) {
        this._params = value;
    }

    public get component(): any {
        return this._component;
    }

    public set component(value: any) {
        this._component = value;
    }

    public get componentParam(): any {
        return this._componentParam;
    }

    public set componentParam(value: any) {
        this._componentParam = value;
    }
}
