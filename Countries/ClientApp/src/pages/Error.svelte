<script lang="ts">
    import {onMount} from "svelte";
    import Card from "../lib/Card.svelte";

    //Tools
    import Condition from "../lib/tools/Condition.js";

    //Services
    import RoutingService from "../lib/services/RoutingService";

    let message = "";

    onMount(async () => {
        const routingService = new RoutingService();

        message = routingService.paramObject.message as string;
        message = message.replaceAll("%20", " ");
    });

</script>

<div on:click={() => RoutingService.goto("/home")}>
    <Card>
        - Back to home
    </Card>
</div>

{#if Condition.isStringNotEmpty(message)}
    <Card style="background-color: #9d1b1b; color: white;">{message}</Card>
{/if}