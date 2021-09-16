// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 
namespace FestivalManager.Tests
{
    using FestivalManager.Entities;
    using NUnit.Framework;
    using System;

    [TestFixture]
	public class StageTests
    {
		private Stage stage = null;

		[SetUp]
		public void InitTest() {
			stage = new Stage();
		}

		[Test]
	    public void AddPerformer_CanNotBeNull()
	    {
			Assert.Throws<ArgumentNullException>(()=> stage.AddPerformer(null));
		}

		[Test]
		public void AddPerformer_ThrowsException()
		{
			Exception ex = Assert.Throws<ArgumentNullException>(() =>
			{
				stage.AddPerformer(null);
			});

			Assert.AreEqual(ex.Message, "Can not be null! (Parameter 'performer')");
		}

		[Test]
		public void AddPerformerAge_CanNotBeLessThanEighteen()
		{
			Assert.Throws<ArgumentException>(() => stage.AddPerformer(new Performer("Pesho", "Georgiev", 15)));
		}

		[Test]
		public void AddPerformer_Successful()
		{
			stage.AddPerformer(new Performer("Pesho", "Georgiev", 20));
			Assert.That(stage.Performers.Count, Is.EqualTo(1));
		}

		[Test]
		public void AddSong_CanNotBeNull()
		{
			Assert.Throws<ArgumentNullException>(() => stage.AddSong(null));
		}

		[Test]
		public void AddSong_CanNotBeLessThanOneMinute()
		{
			Assert.Throws<ArgumentException>(() => stage.AddSong(new Song("Despacito",new TimeSpan(0,0,34))));
		}

		[Test]
		public void AddSongToPerformer_SongNameCanNotBeNull()
		{
			Assert.Throws<ArgumentNullException>(() => stage.AddSongToPerformer(null,"Justin"));
		}

		[Test]
		public void AddSongToPerformer_PerformerNameCanNotBeNull()
		{
			Assert.Throws<ArgumentNullException>(() => stage.AddSongToPerformer("Despasito", null));
		}

		[Test]
		public void AddSongToPerformer_Successful()
		{
			var performer = new Performer("Pesho", "Georgiev", 20);
			var song = new Song("Despacito", new TimeSpan(0, 3, 34));
			stage.AddPerformer(performer);
			stage.AddSong(song);

			stage.AddSongToPerformer("Despacito", "Pesho Georgiev");

			Assert.That(performer.SongList.Count, Is.EqualTo(1));
		}

		[Test]
		public void StagePlay_Successful()
		{
			var performer = new Performer("Pesho", "Georgiev", 20);
			var song = new Song("Despacito", new TimeSpan(0, 3, 34));
			stage.AddPerformer(performer);
			stage.AddSong(song);

			stage.AddSongToPerformer("Despacito", "Pesho Georgiev");

			string result = stage.Play();

			Assert.That(result, Is.EqualTo("1 performers played 1 songs"));
		}

		[Test]
		public void AddSongToPerformer_SongNotExists()
		{
			var performer = new Performer("Pesho", "Georgiev", 20);
			stage.AddPerformer(performer);

			Assert.Throws<ArgumentException>(()=> stage.AddSongToPerformer("Despacito", "Pesho Georgiev"));
		}

		[Test]
		public void AddSongToPerformer_PerformerNotExists()
		{
			var song = new Song("Despacito", new TimeSpan(0, 3, 34));
			stage.AddSong(song);

			Assert.Throws<ArgumentException>(() => stage.AddSongToPerformer("Despacito", "Pesho Georgiev"));
		}
	}
}