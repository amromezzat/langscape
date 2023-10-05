import { makeAutoObservable } from "mobx";
import { FlashCardSet } from "../models/flashCards/flashCardSet";
import api from "../services/api";

export default class FlashCardStore {
    cardsSets: FlashCardSet[] = [];
    loading = false;

    constructor() {
        makeAutoObservable(this);
    }

    setLoading = (state: boolean) => this.loading = state;

    loadSets = async () => {
        this.setLoading(true);
        try {
            this.setSets(await api.FlashCards.get());
            this.setLoading(false);
        }
        catch(error) {
            console.log(error);
            this.setLoading(false);
        }
    }

    private setSets = (sets: FlashCardSet[]) => {
        this.cardsSets = sets;
    }
}