<script>
    export let absolute = false;
    export let scroll = false;
    export let padding = "10px";
    export let margin = "0.4rem";
    export let style = "";

    export let width = 200;

    function shawScroller() {
        return scroll ? "card__scroller" : "";
    }
</script>

<style>
    .card {
        background: #fff;
        border-radius: 5px;
        display: block;
        padding: 10px;
        margin: 1rem;
    }

    .card_less {
        background: #fff;
        border-radius: 5px;
        display: block;
    }

    .card_absol {
        background: #fff;
        border-radius: 5px;
        display: block;
        position: absolute;
        z-index: 1;
    }


    .card-1 {
        box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
        transition: all 0.3s cubic-bezier(0.25, 0.8, 0.25, 1);
    }

    .card-1:hover {
        box-shadow: 0 10px 20px rgba(0, 0, 0, 0.15), 0 5px 5px rgba(0, 0, 0, 0.12);
    }

</style>

{#if absolute}
    <div
            class="card_absol card-1 {shawScroller()}"
            style="margin:{margin}; padding:{padding}; width:{width}px;">
        <slot />
    </div>
{:else}
    <div class="card_less card-1 {shawScroller()}" style="margin:{margin}; padding:{padding}; {style}">
        <slot />
    </div>
{/if}
