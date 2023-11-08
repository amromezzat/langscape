import { observer } from "mobx-react-lite";
import CardGroupComponent from "../../../components/features/flashCards/dashboard/CardGroupComponent";
import CardSetFilter from "../../../components/features/flashCards/dashboard/CardSetFilter";
import { useParams } from "react-router-dom";
import UserHeaderComponent from "../../../components/common/headers/UserHeaderComponent";
import { useEffect } from "react";
import { useStore } from "../../../stores/core/store";

export default observer (function FlashCardSetDashboard() {
    const { username } = useParams();
    const { flashCardStore: {clearFilter}} = useStore();

    useEffect(() => {
        if(!username) {
            clearFilter();
        }    
    }, [username, clearFilter])

    return (
        <>
            { username ? (
                <UserHeaderComponent username={username} />
            ) : (
                <CardSetFilter />
            )}
            <CardGroupComponent />
        </>
    )
});