import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { NgFor } from '@angular/common';
import { GameService } from '../../services/game.service';

@Component({
  selector: 'app-leaderboard',
  standalone: true,
  imports: [MatCardModule, MatListModule, MatIconModule, NgFor],
  templateUrl: './leaderboard.component.html',
  styleUrl: './leaderboard.component.scss'
})
export class LeaderboardComponent {
  items: { userName: string; bestGuessCount: number }[] = [];
  constructor(private game: GameService) {}
  ngOnInit() {
    this.game.leaderboard(10).subscribe(res => this.items = res);
  }
}
