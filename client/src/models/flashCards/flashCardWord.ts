export interface NewFlashCardWord {
    word: string;
    translation: string;
    position: number;
}

export interface FlashCardWord extends NewFlashCardWord {
    id: string;
}