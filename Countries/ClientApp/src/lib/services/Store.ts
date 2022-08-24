import {writable} from "svelte/store";
import Condition from "../tools/Condition";

localStorage.setItem("devUrl", "https://localhost:7008");

export default class Store {

    private static store: any = writable({devUrl: "https://localhost:7008"});

    constructor() {
    }

    public static getByName(objectName: string): any {
        let data = undefined;
        this.store.subscribe((s: any) => data = s[objectName]);
        return data;
    }

    public static addObjectAsync(objectName: string, object: any) {

        if (object) {
            if (this.notExist(objectName)) {
                this.store.subscribe((s: any) => {
                    s[objectName] = object;

                    //Only precisest primitive data to the local store
                    if (typeof object !== "object" && localStorage.getItem(objectName) === null) {
                        localStorage.setItem(objectName, object);
                    }
                });
            }
        } else {
            console.error("Error in store! Please provide object name and object!")
        }
    }

    public static removeByNames(...objectNames: string[]) {
        if (objectNames && objectNames.length > 0) {
            objectNames.forEach((objectName) => {
                this.removeByName(objectName);
            });
        } else {
            console.error("Error in store! Please provide object name!")
        }
    }

    public static removeByName(objectName: string) {
        if (objectName) {
            this.store.subscribe((s: any) => {
                if (s[objectName]) {
                    delete s[objectName];

                    if (localStorage.getItem(objectName) !== null) {
                        localStorage.removeItem(objectName);
                    }
                } else {
                    console.warn("Was unable to remove object. The object mite not exist.");
                }
            });
        } else {
            console.error("Error in store! Please provide object name!")
        }
    }

    public static notExist(objectName: string) {
        return !this.exist(objectName);
    }

    public static exist(objectName: string) {
        let exist = false

        if (Condition.isStringEmpty(objectName)) return exist;

        this.store.subscribe((s: any) => {
            if (s[objectName]) {
                exist = true;
            }
        });

        return exist;
    }

    public static notExistInLocStore(objectName: string) {
        return !this.existInLocStore(objectName);
    }

    public static existInLocStore(objectName: string): boolean {

        if (Condition.isStringEmpty(objectName)) return false;

        return Condition.isStringNotEmpty(localStorage.getItem(objectName));
    }

    public static debugStore(): void {
        this.store.subscribe((s: any) => {
            console.log(s);
        });
    }
}

for (let i = 0; i < localStorage.length; i++) {
    const key = localStorage.key(i);
    Store.addObjectAsync(key, localStorage.getItem(key));
}
