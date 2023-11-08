import { makeAutoObservable, reaction, runInAction } from "mobx";
import { FlashCardSet } from "../../models/flashCards/flashCardSet";
import { flashCardApi } from "../../services/api/features/flashcards/flashCardApi";

export default class FlashCardStore {
    setsRegistery = new Map<string, FlashCardSet>();
    loading = false;
    submittingFavoriteSetsId = new Set<string>();
    filters = new Map<string, any>();

    constructor() {
        makeAutoObservable(this);

        reaction (
            () => this.filters.keys(),
            () => {
                this.setsRegistery.clear();
                this.loadSets();
            }
        )
    }

    setLoading = (state: boolean) => this.loading = state;

    loadSets = async () => {
        this.setLoading(true);
        try {
            const filters = this.filters.size > 0 ? this.filters : undefined;
            this.updateSetsRegistery(await flashCardApi.getCardSets(filters));
        } catch (error) {
            console.log(error);
        } 
        runInAction(() => this.setLoading(false));
    }

    loadSet = async (setId: string) => {
        this.setLoading(true);
        try {
            let set = await flashCardApi.getCardSet(setId);
            this.updateSetRegistery({...this.setsRegistery.get(setId), ...set} as FlashCardSet);
            runInAction(() => this.setLoading(false));
            return set;
        } catch (error) {
            console.log(error);
            runInAction(() => this.setLoading(false));
        }
    }

    addFavoriteSet = async (setId: string) => {
        this.submittingFavoriteSetsId.add(setId);
        try {
            await flashCardApi.addFavoriteSet(setId);
            this.updateSetRegistery({...this.setsRegistery.get(setId), isFavorite: true} as FlashCardSet);

        } catch (error) {
            console.log(error);
        }
        runInAction(() => this.submittingFavoriteSetsId.delete(setId));
    }

    removeFavoriteSet = async (setId: string) => {
        this.submittingFavoriteSetsId.add(setId);
        try {
            await flashCardApi.removeFavoriteSet(setId);
            this.updateSetRegistery({...this.setsRegistery.get(setId), isFavorite: false} as FlashCardSet);
        } catch (error) {
            console.log(error);
        }
        runInAction(() => this.submittingFavoriteSetsId.delete(setId));
    }

    setFilter = (key: string, value?: string) => {
        this.clearFilter();
        this.filters.set(key, value ?? true);
    }

    clearFilter = () => {
        this.filters.clear();
    }

    private updateSetsRegistery = (newSets: FlashCardSet[]) => {
        newSets.forEach(set => {
            set.meta.createdAt = new Date(set.meta.createdAt + 'Z');
            set.previewWords = set.words;
            set.words = [];
            this.updateSetRegistery(set)
        });
    }

    private updateSetRegistery = (newSet: FlashCardSet) => {
        this.setsRegistery.set(newSet.id, newSet);
    }
}