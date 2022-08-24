import Store from './Store'
import RoutingService from "./RoutingService";

export default class Service {

    private static readonly fetch = window.fetch?.bind(window);
    private static readonly url = Store.getByName('devUrl');

    public static async GetAll() {
        try {
            const response = await this.fetch(`${this.url}`);

            if (!response.ok) {
                RoutingService.goto(`/error?message=${await response.text()}`);
                return;
            }

            return await response.json();
        } catch (err) {
            console.error('Error', err);
            RoutingService.goto("/error?message=Something went wrong!");
        }
    }

    public static async GetCountriesByName(name: string) {
        try {
            const response = await this.fetch(`${this.url}/name/${name}`);

            if (!response.ok) {
                RoutingService.goto(`/error?message=${await response.text()}`);
                return;
            }

            return await response.json();
        } catch (err) {
            console.error('Error', err);
            RoutingService.goto("/error?message=Something went wrong!");
        }
    }

    public static async GetCountriesByRegion(region: string) {
        try {
            const response = await this.fetch(`${this.url}/region/${region}`);

            if (!response.ok) {
                RoutingService.goto(`/error?message=${await response.text()}`);
                return;
            }

            return await response.json();
        } catch (err) {
            console.error('Error', err);
            RoutingService.goto("/error?message=Something went wrong!");
        }
    }

    public static async GetCountriesBySubregion(subregion: string) {
        try {
            const response = await this.fetch(`${this.url}/subregion/${subregion}`);

            if (!response.ok) {
                RoutingService.goto(`/error?message=${await response.text()}`);
                return;
            }

            return await response.json();
        } catch (err) {
            console.error('Error', err);
            RoutingService.goto("/error?message=Something went wrong!");
        }
    }

    public static async GetStartWarsPeople() {
        try {
            const response = await this.fetch(`${this.url}/swapi/people`);

            if (!response.ok) {
                RoutingService.goto(`/error?message=${await response.text()}`);
                return;
            }

            return await response.json();
        } catch (err) {
            console.error('Error', err);
            RoutingService.goto("/error?message=Something went wrong!");
        }
    }

    public static async Search(query: string) {
        try {
            const response = await this.fetch(`${this.url}/chuck/search?query=${query}`);

            if (!response.ok) {
                RoutingService.goto(`/error?message=${await response.text()}`);
                return;
            }

            return await response.json();
        } catch (err) {
            console.error('Error', err);
            RoutingService.goto("/error?message=Something went wrong!");
        }
    }
}
