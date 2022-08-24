export default class Condition {

    constructor() {}

    public static hasSomeValue(val: any): boolean {
        return !this.isNothing(val);
    }

    public static isNothing(val: any): boolean {
        let isNothing = true;

        if (this.isUndefined(val) || this.isNull(val)) return isNothing;

        if (val instanceof Promise)
            return false;

        if (typeof val === 'boolean')
            return false;

        for (let key in val) {
            if (val.hasOwnProperty(key)) return !isNothing;
        }

        if (typeof val === 'number')
            isNothing = false;

        return isNothing;
    }

    public static isArrayNotEmpty(array: any[]): boolean {
        return !this.isArrayEmpty(array);
    }

    public static isArrayEmpty(array: any[]): boolean {
        let isEmpty = true;

        if (this.isUndefined(array) || this.isNull(array)) return isEmpty;

        if (array.length === 0) return isEmpty;

        for (const val of array) {
            if (this.hasSomeValue(val)) return false;
        }

        return isEmpty;
    }

    public static isStringNotEmpty(str: string | undefined) {
        return !this.isStringEmpty(str);
    }

    public static isStringEmpty(str: string | undefined) {
        return (str === "" || this.isUndefined(str) || this.isNull(str));
    }

    public static stringContainNot(str: string, containValue:string) {
        return !this.stringContain(str, containValue);
    }

    public static stringContain(str: string, containValue: string) {
        return (this.isStringNotEmpty(str) && this.isStringNotEmpty(containValue) && str.includes(containValue));
    }

    public static isNotUndefined(val: any) {
        return !this.isUndefined(val);
    }

    public static isUndefined(val: any) {
        return (val === undefined);
    }

    public static isNotNull(val: any) {
        return !this.isNull(val);
    }

    public static isNull(val: any) {
        return (val === null);
    }

    public static isNotZero(val: string | number) {
        return !this.isZero(val);
    }

    public static isZero(val: string | number) {
        return (val === 0 || val === "0");
    }
}
