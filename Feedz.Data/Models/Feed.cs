﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.Syndication;

namespace Feedz.Data.Models
{
    public class Feed
    {
        // Identity
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Uri Uri { get; set; }
        public Uri? WebsiteUri { get; set; }
        public Uri? ImageUri { get; set; }
        public string Title { get; set; }

        // Relations
        public List<FeedEntry> Items { get; set; } = new List<FeedEntry>();

        // Timestamps
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public DateTime? LastFetchDate { get; set; }

        public Feed()
        {
        }

        public Feed(SyndicationFeed originalFeed)
        {
            Uri = originalFeed.BaseUri;
            Title = originalFeed.Title.Text;
            ImageUri = originalFeed.ImageUrl;
            LastFetchDate = DateTime.Now;
            RegistrationDate = DateTime.Now;

            foreach (var originalFeedItem in originalFeed.Items)
            {
                Items.Add(new FeedEntry()
                {
                    Id = Guid.NewGuid(),
                    Title = originalFeedItem.Title.Text,
                    Description = originalFeedItem.Summary.Text,
                    PublicationDate = originalFeedItem.PublishDate.UtcDateTime,
                    Uri = originalFeedItem.Links.First().Uri,
                    FeedId = this.Id
                });
            }
        }
    }
}

