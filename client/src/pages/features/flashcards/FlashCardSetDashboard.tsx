import { observer } from "mobx-react-lite";
import CardGroupComponent from "../../../components/features/flashCards/CardGroupComponent";
import CardSetFilter from "../../../components/features/flashCards/CardSetFilter";

export default observer (function FlashCardSetDashboard() {
    return (
        <>
            <CardSetFilter />
            <CardGroupComponent />
        </>
    )
});