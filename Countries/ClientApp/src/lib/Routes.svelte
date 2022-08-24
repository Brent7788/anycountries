<script lang="ts">
    //Svelte
    import {beforeUpdate} from "svelte";
    //Platform
    import Condition from "./tools/Condition";
    import RouteComponent from "./models/RouteComponent";
    import RoutingService from "./services/RoutingService";

    export let components: RouteComponent[];
    export let routingService: RoutingService | undefined;
    let displayComponent: any | undefined;
    let componentParam: any | undefined;
    let is404 = false;

    beforeUpdate(() => {

        if (Condition.isNotUndefined(routingService) && Condition.isNothing(routingService?.fullPath)) {
            routingService.fullPath = '#/';
        }

        if (Condition.isNothing(displayComponent) &&
            Condition.isNotUndefined(routingService) &&
            Condition.hasSomeValue(routingService.fullPath)) {

            const component = getRoutingComponentByPath();
            displayComponent = component?.component;
            componentParam = component?.componentParam;
        }
    });

    function getRoutingComponentByPath(): RouteComponent | undefined {
        let componentToDisplay: RouteComponent;

        for (const component of components) {
            componentToDisplay = getComponentIfRouteMatchFullPath(component);

            if (Condition.isNotUndefined(componentToDisplay))
                return componentToDisplay;
        }
        is404 = true;
        return;
    }

    function getComponentIfRouteMatchFullPath(component: RouteComponent): RouteComponent | undefined {
        if (component.route === routingService.fullPath) {
            is404 = false;
            return component;
        } else if (Condition.stringContain(routingService.fullPath, component.route) &&
            component.isParameterInFullPath(routingService.fullPath)) {
            is404 = false;
            return component;
        }

        is404 = true;
        return undefined;
    }
</script>

{#if displayComponent && Condition.isUndefined(componentParam) && !is404}
    <svelte:component this={displayComponent}/>
{:else if displayComponent && Condition.isNotUndefined(componentParam) && !is404}
    <svelte:component this={displayComponent} param={componentParam}/>
{:else if is404}
    <h1>404</h1>
    <p style="color: red">Page does not exist</p>
{/if}
