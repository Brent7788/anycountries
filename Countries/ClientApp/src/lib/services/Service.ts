import Store from './Store'
import RoutingService from "./RoutingService";

export default class Service {

    private static readonly fetch = window.fetch?.bind(window);
    private static readonly url = Store.getByName('devUrl');
    private static readonly baseNorrisUrl = "https://api.chucknorris.io";

    public static async GetChuckNorrisCategories() {
        try {
            const response = await this.fetch(`${this.url}/chuck/categories`);

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

    public static async GetChuckNorrisJokesByCategory(category: string) {
        try {
            const response = await this.fetch(`${this.baseNorrisUrl}/jokes/random?category=${category}`);

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