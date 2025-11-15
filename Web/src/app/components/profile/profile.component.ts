import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatListModule } from '@angular/material/list';
import { GameService } from '../../services/game.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatListModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {
  best: number | null = null;
  constructor(private game: GameService) {}
  ngOnInit() {
    this.game.myStats().subscribe(res => this.best = res.bestGuessCount);
  }
}
